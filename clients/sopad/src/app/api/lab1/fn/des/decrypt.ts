/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { DecryptRequest } from '../../models/decrypt-request';
import { DecryptResponse } from '../../models/decrypt-response';

export interface Decrypt$Params {
      body?: DecryptRequest
}

export function decrypt(http: HttpClient, rootUrl: string, params?: Decrypt$Params, context?: HttpContext): Observable<StrictHttpResponse<DecryptResponse>> {
  const rb = new RequestBuilder(rootUrl, decrypt.PATH, 'post');
  if (params) {
    rb.body(params.body, 'application/*+json');
  }

  return http.request(
    rb.build({ responseType: 'json', accept: 'text/json', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<DecryptResponse>;
    })
  );
}

decrypt.PATH = '/decrypt';
