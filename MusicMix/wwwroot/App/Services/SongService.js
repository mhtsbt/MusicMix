"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
const core_1 = require("@angular/core");
const http_1 = require("@angular/http");
const Observable_1 = require("rxjs/Observable");
let SongService = class SongService {
    constructor(http) {
        this.http = http;
        this.history = [];
    }
    addToHistory(song) {
        this.history.push(song);
    }
    getNextSong() {
        return Observable_1.Observable.create(obs => {
            this.http.post("/api/nextSong", this.history).subscribe(res => { obs.next(res.json()); this.addToHistory(res.json()); });
        });
    }
    listSong() {
        return Observable_1.Observable.create(obs => {
            this.http.get("/api/song").subscribe(res => { obs.next(res.json()); });
        });
    }
};
SongService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], SongService);
exports.SongService = SongService;
//# sourceMappingURL=SongService.js.map