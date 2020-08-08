import { Injectable, Inject } from '@angular/core';
import { CacheFactory, Cache, CacheOptions } from 'cachefactory';
import { CacheService } from './cache.service';
import { CACHE_OPTIONS } from '../CacheOptions';

@Injectable({
    providedIn: 'root'
})
export class CachefactoryService extends CacheService<string> {
    private factory: CacheFactory = new CacheFactory();
    private cache: Cache;

    constructor(@Inject(CACHE_OPTIONS) options: CacheOptions) {
        super(options);

        this.cache = this.factory.createCache('', this.options);
    }

    get<T = any>(id: string): T {
        return this.cache.get(id);
    }

    remove<T = any>(id: string): T {
        return this.cache.remove(id);
    }

    clear(): boolean {
        this.cache.removeAll();
        return true;
    }

    has(id: string): boolean {
        const keys = this.cache.keys();
        if (!(keys && keys.length)) {
            return false;
        }

        return keys.includes(id);
    }

    set<T = any>(id: string, object: T): T {
        this.cache.put(id, object);
        return object;
    }
}
