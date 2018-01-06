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


}
