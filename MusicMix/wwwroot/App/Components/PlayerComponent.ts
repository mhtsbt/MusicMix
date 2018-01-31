import { Component, OnInit, ViewContainerRef, EventEmitter } from '@angular/core';

declare var $;

@Component({
    moduleId: module.id,
    selector: 'app-player',
    templateUrl: './PlayerComponent.html',
    inputs: ["song"],
    outputs: ["fading", "finished"]
})
export class PlayerComponent implements OnInit {

    public song: any = { title: "A", artist: "x", fileName: "x.mp3", startTime: 0 };
    public player;
    public timeLeft = 0;
    public flow;
    public finished = new EventEmitter<Boolean>();
    public fading = new EventEmitter<Boolean>();

    public fadeoutActive: Boolean = false;

    constructor(private viewContainerRef: ViewContainerRef) { }

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
        }

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

    public skipSong() {
        console.log("skip song");
        this.fadeOut();
    }

    public async fadeOut() {

        this.fadeoutActive = true;

        this.fading.emit(true);

        var ctrl = this;

        $(this.player).animate({ volume: 0 }, 10000, function () {

            console.log("finished song");
            ctrl.finished.emit(true);
            ctrl.player.pause();
            this.fadeoutActive = false;

        });

    }

    public async fadeIn() {

        var ctrl = this;

        this.player.volume = 0;
        ctrl.player.play();

        $(this.player).animate({ volume: 1 }, 10000, function () {
            console.log("starting song");
        });

    }

}