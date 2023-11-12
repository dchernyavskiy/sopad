import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DiffieHellmanService {
  private readonly DH_KEY_LENGTH: number = 16;
  private readonly P: bigint = 2n ** 128n - 159n;
  private readonly G: bigint = 5n;
  publicKey: bigint = 0n;
  privateKey: bigint = 0n;
  secret: bigint = 0n;

  constructor() {
    this.generateKeyPair();
  }

  update(secret: bigint) {
    this.secret = this.modPow(secret, this.privateKey, this.P);
    return this.secret;
  }

  generateKeyPair() {
    const privateKey: bigint[] = [];

    crypto.getRandomValues(new Uint8Array(this.DH_KEY_LENGTH)).forEach(value => {
      privateKey.push(BigInt(value));
    });

    let privateK: bigint = 0n;
    for (let i = 0; i < this.DH_KEY_LENGTH; i++) {
      privateK |= BigInt(privateKey[i]) << BigInt(8 * i);
    }

    this.privateKey = privateK;
    this.publicKey = this.modPow(this.G, this.privateKey, this.P);
    this.resetSecret();
  }

  resetSecret(){
    this.secret = this.modPow(this.publicKey, this.privateKey, this.P);
  }

  private modPow(base: bigint, exponent: bigint, modulus: bigint): bigint {
    if (modulus === 1n) return 0n;

    let result: bigint = 1n;
    //base = base % modulus;

    while (exponent > 0n) {
      result *= base;
      exponent >>= 1n;
    }
    // while (exponent > 0n) {
    //   if (exponent % 2n === 1n) {
    //     result = (result * base) % modulus;
    //   }
    //   exponent = exponent >> 1n;
    //   base = (base * base) % modulus;
    // }

    return result % modulus;
  }

  private toByteArray(value: bigint): number[] {
    const byteArray: number[] = [];
    while (value > 0n) {
      byteArray.push(Number(value & 0xFFn));
      value >>= 8n;
    }
    return byteArray.reverse();
  }

  private hexToBytes(hex: string): Uint8Array {
    const bytes = new Uint8Array(hex.length / 2);
    for (let i = 0; i < bytes.length; i++) {
      bytes[i] = parseInt(hex.substr(i * 2, 2), 16);
    }
    return bytes;
  }

  private toHex(bytes: Uint8Array): string {
    return Array.from(bytes).map(byte => byte.toString(16).padStart(2, '0')).join('');
  }

  async decryptMessage(encryptedMessage: string): Promise<string> {
    const sharedSecret: bigint = this.secret;

    const encryptedMessageBytes: Uint8Array = this.hexToBytes(encryptedMessage);
    const iv: Uint8Array = encryptedMessageBytes.slice(0, 12);
    const ciphertext: Uint8Array = encryptedMessageBytes.slice(12);

    const decryptedBytes: Uint8Array = new Uint8Array(await crypto.subtle.decrypt(
      {name: 'AES-GCM', iv},
      await crypto.subtle.importKey(
        'raw',
        await crypto.subtle.digest('SHA-256', new Uint8Array(this.toByteArray(sharedSecret))),
        {name: 'AES-GCM'},
        true,
        ['decrypt']
      ),
      ciphertext
    ));

    return new TextDecoder().decode(decryptedBytes);
  }

  async encryptMessage(message: string): Promise<string> {
    const sharedSecret: bigint = this.secret;

    const iv: Uint8Array = crypto.getRandomValues(new Uint8Array(12));
    const messageBytes: Uint8Array = new TextEncoder().encode(message);

    const ciphertext: ArrayBuffer = await crypto.subtle.encrypt(
      {name: 'AES-GCM', iv},
      await crypto.subtle.importKey(
        'raw',
        await crypto.subtle.digest('SHA-256', new Uint8Array(this.toByteArray(sharedSecret))),
        {name: 'AES-GCM'},
        true,
        ['encrypt']
      ),
      messageBytes
    );
    const encryptedMessage: Uint8Array = new Uint8Array([...iv, ...new Uint8Array(ciphertext)]);

    return this.toHex(encryptedMessage);
  }
}

export interface SecretPackage {
  secret: string;
  isFinished: boolean;
}
