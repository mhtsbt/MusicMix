import { Component, OnInit } from '@angular/core';
import { SongService } from "../Services/SongService";

@Component({
    moduleId: module.id,
    selector: 'app-controls',
    templateUrl: './ControlsComponent.html'
})
export class ControlsComponent {

    

    constructor(private songService: SongService) {

      
    }

   

    //private sleep(ms) {
    //    return new Promise(resolve => setTimeout(resolve, ms));
    //}

    //public async mix() {

    //    var source = this.player_a;
    //    var target = this.player_b;

    //    target.play();

    //    for (var i = 0; i < 100; i++) {
    //        await this.sleep(50);
    //        source.volume -= 0.01;
    //        target.volume += 0.01;
    //    }

    //}

}
