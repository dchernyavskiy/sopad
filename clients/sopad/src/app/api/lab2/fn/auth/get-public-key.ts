/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { RsaPublicKey } from '../../models/rsa-public-key';

export interface GetPublicKey$Params {
}

export function getPublicKey(http: HttpClient, rootUrl: string, params?: GetPublicKey$Params, context?: HttpContext): Observable<StrictHttpResponse<RsaPublicKey>> {
  const rb = new RequestBuilder(rootUrl, getPublicKey.PATH, 'post');
  if (params) {
  }

  return http.request(
    rb.build({ responseType: 'json', accept: 'text/json', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<RsaPublicKey>;
    })
  );
}

getPublicKey.PATH = '/get-public-key';
