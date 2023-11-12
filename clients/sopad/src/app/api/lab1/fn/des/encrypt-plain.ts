/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { EncryptRequest } from '../../models/encrypt-request';
import { EncryptResponse } from '../../models/encrypt-response';

export interface Encrypt$Plain$Params {
      body?: EncryptRequest
}

export function encrypt$Plain(http: HttpClient, rootUrl: string, params?: Encrypt$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<EncryptResponse>> {
  const rb = new RequestBuilder(rootUrl, encrypt$Plain.PATH, 'post');
  if (params) {
    rb.body(params.body, 'application/*+json');
  }

  return http.request(
    rb.build({ responseType: 'text', accept: 'text/plain', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<EncryptResponse>;
    })
  );
}

encrypt$Plain.PATH = '/encrypt';
