import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { SharedModule } from './shared/shared.module';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { ShellModule } from './views/shell/shell.module';
import { CacheModule } from './core/cache/cache.module';
import { AuthModule } from './core/auth/auth.module';
import { HttpModule } from './core/http';
import { SERVICES } from './core/services';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MaterialModule } from './shared';
import { NumericPipe } from './pipes/numericPipe';
import { OrderByPipe } from './pipes/orderByPipe';

@NgModule({
  declarations: [
    AppComponent,
    NumericPipe,
    OrderByPipe
  ],
  exports: [SharedModule],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    ShellModule,
    CacheModule.forRoot(),
    AuthModule.forRoot(),
    HttpModule,
    FontAwesomeModule,
    MaterialModule
  ],
  providers: [SERVICES],
  bootstrap: [AppComponent],
})
export class AppModule { }
