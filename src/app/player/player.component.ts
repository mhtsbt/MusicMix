import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-player',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.css'],
  inputs: ["song"]
})
export class PlayerComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
