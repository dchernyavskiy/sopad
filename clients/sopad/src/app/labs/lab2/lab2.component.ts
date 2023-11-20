import {Component} from '@angular/core';
import * as forge from "node-forge";
import {RsaService} from 'src/app/services/rsa.service';
import {AuthService} from "../../api/lab2/services/auth.service";

@Component({
  selector: 'app-lab2',
  templateUrl: './lab2.component.html',
  styleUrls: ['./lab2.component.scss']
})
export class Lab2Component {
  password: string = '';
  login: string = '';

  constructor(private readonly authService: AuthService, private readonly rsaService: RsaService) {
  }

  submit() {
    this.authService.getPublicKey()
      .subscribe(res => {
        this.rsaService.generateKeys();
        const publicKey = forge.pki.publicKeyFromPem(res.publicKey!);
        const privateKey = forge.pki.privateKeyFromPem(this.rsaService.privateKey);
        console.log('private', this.rsaService.privateKey)
        console.log('public', this.rsaService.publicKey)
        const request = {
          encryptedLogin: forge.util.encode64(publicKey.encrypt(this.login, 'RSAES-PKCS1-V1_5')),
          encryptedPassword: forge.util.encode64(publicKey.encrypt(this.password, 'RSAES-PKCS1-V1_5')),
          publicKey: this.rsaService.publicKey
        };
        this.authService.login({
          body: request
        }).subscribe(res => {
          const decryptedLogin = privateKey.decrypt(forge.util.decode64(res.login!), 'RSAES-PKCS1-V1_5');
          const decryptedPassword = privateKey.decrypt(forge.util.decode64(res.password!), 'RSAES-PKCS1-V1_5');
          alert(`Your login: ${decryptedLogin}. Your password: ${decryptedPassword}`)
        })
      })
  }
}
