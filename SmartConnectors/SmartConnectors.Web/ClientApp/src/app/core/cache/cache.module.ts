import { ModuleWithProviders, NgModule } from '@angular/core';
import { CacheOptions } from 'cachefactory';
import { DEFAULT_CACHE_OPTIONS, CACHE_OPTIONS } from './CacheOptions';
import { CachefactoryService } from './services/cachefactory.service';
import { CacheService } from './services/cache.service';

@NgModule({
  imports: [
    // CommonModule
  ],
  declarations: []
})

export class CacheModule {
  static forRoot(options: CacheOptions = DEFAULT_CACHE_OPTIONS): ModuleWithProviders {
    return {
      ngModule: CacheModule,
      providers: [
        { provide: CACHE_OPTIONS, useValue: options },
        { provide: CacheService, useClass: CachefactoryService }
      ]
    };
  }
}
