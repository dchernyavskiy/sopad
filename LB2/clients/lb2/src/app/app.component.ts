import {Component} from '@angular/core';
import {AuthService} from "./api/server/services/auth.service";
import {LoginRequest} from "./api/server/models/login-request";
import * as forge from 'node-forge';
import {environment} from "../environments/environment";


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'lb2';
  password: string = '';
  login: string = '';

  constructor(private readonly authService: AuthService) {
  }

  submit() {
    const publicKey = forge.pki.publicKeyFromPem(environment.PUBLIC_KEY);
    const request = {
      encryptedLogin: forge.util.encode64(publicKey.encrypt(this.login, 'RSA-OAEP')),
      encryptedPassword: forge.util.encode64(publicKey.encrypt(this.password, 'RSA-OAEP'))
    };
    console.log(request)
    this.authService.login({
      body: request
    }).subscribe(res => {
      alert(`Your login: ${res.login}. Your password: ${res.password}`)
    })
  }
}
