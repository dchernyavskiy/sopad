/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';

import { decrypt } from '../fn/des/decrypt';
import { Decrypt$Params } from '../fn/des/decrypt';
import { decrypt$Plain } from '../fn/des/decrypt-plain';
import { Decrypt$Plain$Params } from '../fn/des/decrypt-plain';
import { DecryptResponse } from '../models/decrypt-response';
import { encrypt } from '../fn/des/encrypt';
import { Encrypt$Params } from '../fn/des/encrypt';
import { encrypt$Plain } from '../fn/des/encrypt-plain';
import { Encrypt$Plain$Params } from '../fn/des/encrypt-plain';
import { EncryptResponse } from '../models/encrypt-response';
import { stream } from '../fn/des/stream';
import { Stream$Params } from '../fn/des/stream';
import { stream$Plain } from '../fn/des/stream-plain';
import { Stream$Plain$Params } from '../fn/des/stream-plain';

@Injectable({ providedIn: 'root' })
export class DesService extends BaseService {
  constructor(config: ApiConfiguration, http: HttpClient) {
    super(config, http);
  }

  /** Path part for operation `encrypt()` */
  static readonly EncryptPath = '/encrypt';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `encrypt$Plain()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  encrypt$Plain$Response(params?: Encrypt$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<EncryptResponse>> {
    return encrypt$Plain(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `encrypt$Plain$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  encrypt$Plain(params?: Encrypt$Plain$Params, context?: HttpContext): Observable<EncryptResponse> {
    return this.encrypt$Plain$Response(params, context).pipe(
      map((r: StrictHttpResponse<EncryptResponse>): EncryptResponse => r.body)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `encrypt()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  encrypt$Response(params?: Encrypt$Params, context?: HttpContext): Observable<StrictHttpResponse<EncryptResponse>> {
    return encrypt(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `encrypt$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  encrypt(params?: Encrypt$Params, context?: HttpContext): Observable<EncryptResponse> {
    return this.encrypt$Response(params, context).pipe(
      map((r: StrictHttpResponse<EncryptResponse>): EncryptResponse => r.body)
    );
  }

  /** Path part for operation `decrypt()` */
  static readonly DecryptPath = '/decrypt';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `decrypt$Plain()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  decrypt$Plain$Response(params?: Decrypt$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<DecryptResponse>> {
    return decrypt$Plain(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `decrypt$Plain$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  decrypt$Plain(params?: Decrypt$Plain$Params, context?: HttpContext): Observable<DecryptResponse> {
    return this.decrypt$Plain$Response(params, context).pipe(
      map((r: StrictHttpResponse<DecryptResponse>): DecryptResponse => r.body)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `decrypt()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  decrypt$Response(params?: Decrypt$Params, context?: HttpContext): Observable<StrictHttpResponse<DecryptResponse>> {
    return decrypt(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `decrypt$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  decrypt(params?: Decrypt$Params, context?: HttpContext): Observable<DecryptResponse> {
    return this.decrypt$Response(params, context).pipe(
      map((r: StrictHttpResponse<DecryptResponse>): DecryptResponse => r.body)
    );
  }

  /** Path part for operation `stream()` */
  static readonly StreamPath = '/stream';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `stream$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  stream$Plain$Response(params?: Stream$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<Array<number>>> {
    return stream$Plain(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `stream$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  stream$Plain(params?: Stream$Plain$Params, context?: HttpContext): Observable<Array<number>> {
    return this.stream$Plain$Response(params, context).pipe(
      map((r: StrictHttpResponse<Array<number>>): Array<number> => r.body)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `stream()` instead.
   *
   * This method doesn't expect any request body.
   */
  stream$Response(params?: Stream$Params, context?: HttpContext): Observable<StrictHttpResponse<Array<number>>> {
    return stream(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `stream$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  stream(params?: Stream$Params, context?: HttpContext): Observable<Array<number>> {
    return this.stream$Response(params, context).pipe(
      map((r: StrictHttpResponse<Array<number>>): Array<number> => r.body)
    );
  }

}
