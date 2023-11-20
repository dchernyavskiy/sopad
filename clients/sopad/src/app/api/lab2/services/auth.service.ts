/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';

import { getPublicKey } from '../fn/auth/get-public-key';
import { GetPublicKey$Params } from '../fn/auth/get-public-key';
import { getPublicKey$Plain } from '../fn/auth/get-public-key-plain';
import { GetPublicKey$Plain$Params } from '../fn/auth/get-public-key-plain';
import { login } from '../fn/auth/login';
import { Login$Params } from '../fn/auth/login';
import { login$Plain } from '../fn/auth/login-plain';
import { Login$Plain$Params } from '../fn/auth/login-plain';
import { LoginResponse } from '../models/login-response';
import { RsaPublicKey } from '../models/rsa-public-key';

@Injectable({ providedIn: 'root' })
export class AuthService extends BaseService {
  constructor(config: ApiConfiguration, http: HttpClient) {
    super(config, http);
  }

  /** Path part for operation `login()` */
  static readonly LoginPath = '/login';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `login$Plain()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  login$Plain$Response(params?: Login$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<LoginResponse>> {
    return login$Plain(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `login$Plain$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  login$Plain(params?: Login$Plain$Params, context?: HttpContext): Observable<LoginResponse> {
    return this.login$Plain$Response(params, context).pipe(
      map((r: StrictHttpResponse<LoginResponse>): LoginResponse => r.body)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `login()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  login$Response(params?: Login$Params, context?: HttpContext): Observable<StrictHttpResponse<LoginResponse>> {
    return login(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `login$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  login(params?: Login$Params, context?: HttpContext): Observable<LoginResponse> {
    return this.login$Response(params, context).pipe(
      map((r: StrictHttpResponse<LoginResponse>): LoginResponse => r.body)
    );
  }

  /** Path part for operation `getPublicKey()` */
  static readonly GetPublicKeyPath = '/get-public-key';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `getPublicKey$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  getPublicKey$Plain$Response(params?: GetPublicKey$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<RsaPublicKey>> {
    return getPublicKey$Plain(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `getPublicKey$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  getPublicKey$Plain(params?: GetPublicKey$Plain$Params, context?: HttpContext): Observable<RsaPublicKey> {
    return this.getPublicKey$Plain$Response(params, context).pipe(
      map((r: StrictHttpResponse<RsaPublicKey>): RsaPublicKey => r.body)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `getPublicKey()` instead.
   *
   * This method doesn't expect any request body.
   */
  getPublicKey$Response(params?: GetPublicKey$Params, context?: HttpContext): Observable<StrictHttpResponse<RsaPublicKey>> {
    return getPublicKey(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `getPublicKey$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  getPublicKey(params?: GetPublicKey$Params, context?: HttpContext): Observable<RsaPublicKey> {
    return this.getPublicKey$Response(params, context).pipe(
      map((r: StrictHttpResponse<RsaPublicKey>): RsaPublicKey => r.body)
    );
  }

}
