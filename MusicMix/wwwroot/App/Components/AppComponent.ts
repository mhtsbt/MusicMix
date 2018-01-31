import { Component, EventEmitter } from "@angular/core";
import { SongService } from "../Services/SongService";

@Component({
    selector: "App",
    templateUrl: "App/Components/AppComponent.html"
})
export class AppComponent {

    public song_a;
    public song_b;

    public currentPosition = 0;

    constructor(private songService: SongService) {

        this.songService.getNextSong().subscribe(res => {
            this.song_a = res; console.log(this.song_a);
        });

    }

    public fading_a() {

        this.songService.getNextSong().subscribe(res => {
            this.song_b = res; console.log(this.song_b);
        });

    }

    public fading_b() {

        this.songService.getNextSong().subscribe(res => {
            this.song_a = res; console.log(this.song_a);
        });

    }

    public skipSong(): void {

    }

}