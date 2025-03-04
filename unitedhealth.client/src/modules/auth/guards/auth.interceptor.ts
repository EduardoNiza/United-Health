import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { AuthService } from "../services/auth.service";
import { environment } from "environments/environment";

// this will intercept all http requests and redirect to signin if the user is not authenticated and
// is trying to access a protected route
@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // add auth header with jwt if user is logged in and request is to the api url
    const user = this.authService.userValue;
    const isLoggedIn = user && user.token;
    const isApiUrl = req.url.startsWith(environment.apiServerUrl) || req.url.startsWith(environment.apiMedicalUrl) || 
                    req.url.startsWith(environment.apiNutritionUrl) || req.url.startsWith(environment.apiTrainingUrl);
    if (isLoggedIn && isApiUrl) {
        req = req.clone({
            setHeaders: {
                Authorization: `Bearer ${user?.token}`
            }
        });
    }

    return next.handle(req).pipe(catchError(err => {
      if ([401, 403].includes(err.status) && this.authService.userValue) {
          // auto logout if 401 or 403 response returned from api
          this.authService.signOut();
      }

      console.error(err);
      return throwError(err.error);
  }));
  }
}
