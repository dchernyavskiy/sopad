import {Component, OnDestroy, OnInit} from '@angular/core';
import {DiffieHellmanService, SecretPackage} from "../../services/diffie-hellman.service";
import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import {login} from "../../api/lab2/fn/auth/login";
import {BehaviorSubject} from "rxjs";

export interface Message {
  text?: string;
  name?: string;
  isMine?: boolean;
}

@Component({
  selector: 'app-lab4',
  templateUrl: './lab4.component.html',
  styleUrls: ['./lab4.component.scss']
})
export class Lab4Component implements OnInit, OnDestroy {
  messages: Message[] = [];
  newMessage: Message = {
    isMine: true,
    name: (Math.random() + 1).toString(36).substring(7)
  };
  private hubConnection: HubConnection = new HubConnectionBuilder()
    .withUrl('http://localhost:5006/hubs/chat')
    .build();

  constructor(private readonly diffieHellmanService: DiffieHellmanService) {
    this.setPackage();

    setInterval(() => {
      console.log(this.diffieHellmanService.secret)
    }, 200)
  }

  setPackage() {
    this.package = {
      secret: this.diffieHellmanService.secret.toString(),
      isFinished: false
    }
  }

  ngOnDestroy(): void {
    this.hubConnection.stop().then(_ => {
    })
  }

  package: SecretPackage | undefined;

  ngOnInit(): void {
    this.hubConnection.on("ping", () => {
      this.diffieHellmanService.generateKeyPair();
    })

    this.hubConnection.on("receiveMessage", (data: Message) => {
      this.diffieHellmanService.decryptMessage(data.text!).then(r => {
        data.text = r;
        this.messages.push(data);
      })
    })

    this.hubConnection.on("newSecret", (data: string) => {
      this.diffieHellmanService.secret = BigInt(data);
    })

    this.hubConnection.on("updateSecret", (data: string) => {
      console.log('received secret', data)
      return this.diffieHellmanService.update(BigInt(data)).toString();
    })

    this.hubConnection.start().then(_ => {
      this.hubConnection.invoke("PingAsync").then(_ => {
        this.hubConnection.send("UpdateAsync", this.diffieHellmanService.secret.toString()).then(_ => {
          console.log('sent')
        })
      })
    });
  }

  update() {
    this.hubConnection.invoke("UpdateSecretAsync", this.package).then(_ => {
      console.log('updated')
    })
  }

  async send() {
    this.messages.push(this.newMessage)
    this.hubConnection.invoke("SendAsync", {
      text: await this.diffieHellmanService.encryptMessage(this.newMessage.text!),
      name: this.newMessage.name
    }).then(_ => {
    })
    this.newMessage = {
      isMine: true,
      name: this.newMessage.name
    }
  }
}
