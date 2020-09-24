import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule  } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { PurchaseClient, API_BASE_URL } from './api.client';
import { PurchaseComponent } from './purchase/purchase.component';
import { ListComponent } from './list/list.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    PurchaseComponent,
    ListComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ReactiveFormsModule ,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'view/:id', component: PurchaseComponent },
      { path: 'list', component: ListComponent }
    ])
  ],
  providers: [PurchaseClient, { provide: API_BASE_URL, useFactory: () => { return document.getElementsByTagName('base')[0].href.slice(0, -1) }, deps: [] }],
  bootstrap: [AppComponent]
})
export class AppModule { }
