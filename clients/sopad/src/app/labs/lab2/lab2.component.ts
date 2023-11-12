import { Component } from '@angular/core';
import * as forge from "node-forge";
import {environment} from "../../../environments/environment";
import {AuthService} from "../../api/lab2/services/auth.service";

@Component({
  selector: 'app-lab2',
  templateUrl: './lab2.component.html',
  styleUrls: ['./lab2.component.scss']
})
export class Lab2Component {
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
