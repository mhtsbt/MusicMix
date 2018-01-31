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
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", { value: true });
const core_1 = require("@angular/core");
let PlayerComponent = class PlayerComponent {
    constructor(viewContainerRef) {
        this.viewContainerRef = viewContainerRef;
        this.song = { title: "A", artist: "x", fileName: "x.mp3", startTime: 0 };
        this.timeLeft = 0;
        this.finished = new core_1.EventEmitter();
        this.fading = new core_1.EventEmitter();
        this.fadeoutActive = false;
    }
    ngOnInit() {
        this.player = $(this.viewContainerRef.element.nativeElement).find("audio").get(0);
        var timeoutId;
        this.player.onplay = () => {
            timeoutId = window.setInterval(() => {
                var position = Math.round(this.player.currentTime);
                this.timeLeft = this.song.stopTime - position;
                if (position >= this.song.stopTime && !this.fadeoutActive) {
                    this.fadeOut();
                }
            }, 500);
        };
        this.player.onended = () => {
            console.log("song ended");
            this.fadeOut();
        };
        this.player.onpause = () => {
            window.clearTimeout(timeoutId);
        };
    }
    ngOnChanges(changes) {
        this.player = $(this.viewContainerRef.element.nativeElement).find("audio").get(0);
        if (this.player && this.song) {
            console.log("SONG CHANGED!");
            this.player.src = "Music/" + this.song.fileName;
            this.fadeIn();
            this.player.currentTime = this.song.startTime;
            this.fadeoutActive = false;
        }
    }
    skipSong() {
        console.log("skip song");
        this.fadeOut();
    }
    fadeOut() {
        return __awaiter(this, void 0, void 0, function* () {
            this.fadeoutActive = true;
            this.fading.emit(true);
            var ctrl = this;
            $(this.player).animate({ volume: 0 }, 10000, function () {
                console.log("finished song");
                ctrl.finished.emit(true);
                ctrl.player.pause();
                this.fadeoutActive = false;
            });
        });
    }
    fadeIn() {
        return __awaiter(this, void 0, void 0, function* () {
            var ctrl = this;
            this.player.volume = 0;
            ctrl.player.play();
            $(this.player).animate({ volume: 1 }, 10000, function () {
                console.log("starting song");
            });
        });
    }
};
PlayerComponent = __decorate([
    core_1.Component({
        moduleId: module.id,
        selector: 'app-player',
        templateUrl: './PlayerComponent.html',
        inputs: ["song"],
        outputs: ["fading", "finished"]
    }),
    __metadata("design:paramtypes", [core_1.ViewContainerRef])
], PlayerComponent);
exports.PlayerComponent = PlayerComponent;
//# sourceMappingURL=PlayerComponent.js.map