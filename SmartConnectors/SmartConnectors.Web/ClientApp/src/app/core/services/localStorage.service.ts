import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})

export class LocalStorageService {
    constructor() { }

    clear() {
        localStorage.clear();
    }

    removeItem(key: string) {
        localStorage.removeItem(key);
    }

    setItem<T>(key: string, data: T): boolean {
        try {
            if (typeof data === "string") {
                localStorage.setItem(key, data)
            }
            else if (typeof data === "number" || typeof data === "boolean") {
                localStorage.setItem(key, data.toString())
            }
            else {
                //its my custom object
                localStorage.setItem(key, JSON.stringify(data));
            }
            return true;
        }
        catch (e) {
            return false;
        }
    }

    getItem<T>(key: string): T {
        try {
            let data: T = null;
           
            data = <T>JSON.parse(localStorage.getItem(key));          

            return data;

        } catch (e) {
            console.log('Error getting data from localStorage', e);
            return null;
        }
    }
}