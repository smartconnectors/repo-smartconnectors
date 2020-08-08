import { Injectable } from '@angular/core';
import { CacheOptions } from '~/core/cache/CacheOptions';

export abstract class CacheService<TKey = string> {
  constructor(protected options: CacheOptions) { }

  abstract get<T = any>(id: TKey): T;
  abstract set<T = any>(id: TKey, object: T): T;
  abstract has(id: TKey): boolean;
  abstract clear(): boolean;
  abstract remove<T = any>(id: TKey): T;
}
