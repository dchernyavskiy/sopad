/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';


export interface Stream$Plain$Params {
}

export function stream$Plain(http: HttpClient, rootUrl: string, params?: Stream$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<Array<number>>> {
  const rb = new RequestBuilder(rootUrl, stream$Plain.PATH, 'get');
  if (params) {
  }

  return http.request(
    rb.build({ responseType: 'text', accept: 'text/plain', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<Array<number>>;
    })
  );
}

stream$Plain.PATH = '/stream';
