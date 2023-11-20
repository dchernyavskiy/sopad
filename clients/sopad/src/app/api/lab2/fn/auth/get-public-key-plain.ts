/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { RsaPublicKey } from '../../models/rsa-public-key';

export interface GetPublicKey$Plain$Params {
}

export function getPublicKey$Plain(http: HttpClient, rootUrl: string, params?: GetPublicKey$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<RsaPublicKey>> {
  const rb = new RequestBuilder(rootUrl, getPublicKey$Plain.PATH, 'post');
  if (params) {
  }

  return http.request(
    rb.build({ responseType: 'text', accept: 'text/plain', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<RsaPublicKey>;
    })
  );
}

getPublicKey$Plain.PATH = '/get-public-key';
