
import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { Observable } from "rxjs/Observable";

@Injectable()
export class SongService {

    public history = [];

    constructor(private http: Http) {

    }

    public addToHistory(song) {
        this.history.push(song);
    }

    public getNextSong() {

        return Observable.create(obs => {
            this.http.post("/api/nextSong", this.history).subscribe(res => { obs.next(res.json()); this.addToHistory(res.json()) });
        });

    }

    public listSong() {

        return Observable.create(obs => {
            this.http.get("/api/song").subscribe(res => { obs.next(res.json()); });
        });

    }

}