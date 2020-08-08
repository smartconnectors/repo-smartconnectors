import { InjectionToken } from '@angular/core';
import { CacheOptions } from 'cachefactory';
export const CACHE_OPTIONS = new InjectionToken<CacheOptions>('CacheOptions');
export { CacheOptions };

export const DEFAULT_CACHE_OPTIONS: CacheOptions = {
  enable: true,
  storageMode: 'localStorage'
};
