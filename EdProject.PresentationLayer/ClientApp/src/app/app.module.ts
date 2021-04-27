import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import {HttpClientModule,HTTP_INTERCEPTORS} from '@angular/common/http';
import { NotFoundComponent } from './error-pages/not-found/not-found.component';
import { MenuComponent } from './menu/menu.component';
import {ErrorHandlerService} from './shared/services/error-handler.service'


@NgModule({
  declarations: [
    AppComponent,
    NotFoundComponent,
    MenuComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    RouterModule.forRoot([
      { path: 'authentication', loadChildren: () => import('./authentication/authentication.module').then(m => m.AuthenticationModule) },
      { path: '404', component : NotFoundComponent},
    ])  
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorHandlerService,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
