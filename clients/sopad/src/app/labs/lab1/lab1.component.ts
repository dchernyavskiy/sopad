import {Component} from '@angular/core';
import {DesService} from "../../api/lab1/services/des.service";
import {DecryptRequest} from "../../api/lab1/models/decrypt-request";
import {EncryptRequest} from "../../api/lab1/models/encrypt-request";

@Component({
  selector: 'app-lab1',
  templateUrl: './lab1.component.html',
  styleUrls: ['./lab1.component.scss']
})
export class Lab1Component {
  encryptRequest: EncryptRequest = {};
  decryptRequest: DecryptRequest = {};
  entropies: [string, number][] = [];

  constructor(private readonly desService: DesService) {
    this.desService.encrypt({
      body: {
        plainText: 'Lorem ipsum dolor sit amet consectetur',
        key: 'secret'
      }
    }).subscribe(res => {
      console.log(res)
    })
  }

  decrypt() {
    this.desService.decrypt({
      body: this.decryptRequest
    }).subscribe(res => {
      console.log(res.decryptedText)
      this.encryptRequest = {
        plainText: res.decryptedText,
        key: this.decryptRequest.key
      }
      this.entropies = this.Object.keys(res.entropies!).map((value, index) => [value, this.Object.values(res.entropies!)[index]])
    })
  }

  encrypt() {
    this.desService.encrypt({
      body: this.encryptRequest
    }).subscribe(res => {
      this.decryptRequest = {
        cipheredText: res.encryptedText,
        key: this.encryptRequest.key
      };
      this.entropies = this.Object.keys(res.entropies!).map((value, index) => [value, this.Object.values(res.entropies!)[index]])
    })
  }

  protected readonly Object = Object;
}
