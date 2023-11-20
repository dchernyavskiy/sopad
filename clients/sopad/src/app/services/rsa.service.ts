import {Injectable} from '@angular/core';
import * as forge from 'node-forge'
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class RsaService {

  privateKey: string = ''
  publicKey: string = ''

  constructor() {
  }

  generateKeys() {
    const keyPair = forge.pki.rsa.generateKeyPair({bits: 2048})
    this.publicKey = forge.pki.publicKeyToPem(keyPair.publicKey)
    this.privateKey = forge.pki.privateKeyToPem(keyPair.privateKey)

    const data = 'fasdfasdf'
    const aa = forge.util.encode64(keyPair.publicKey.encrypt(data, 'RSA-OAEP'))
    const bb = keyPair.privateKey.decrypt(forge.util.decode64(aa), 'RSA-OAEP')
    console.log(bb)
  }
}
