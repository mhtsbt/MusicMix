import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-controls',
  templateUrl: './controls.component.html',
  styleUrls: ['./controls.component.css']
})
export class ControlsComponent implements OnInit {

	public player_a;
	public player_b;

  constructor() { 

  }

  ngOnInit() {

	this.player_a = document.getElementById('pa').firstChild;
	this.player_b = document.getElementById('pb').firstChild;

	this.player_b.volume = 0;
	this.player_b.currentTime = 30;

	}

	private sleep(ms) {
	return new Promise(resolve => setTimeout(resolve, ms));
	}

   public async mix() {

	var source = this.player_a;
	var target = this.player_b;

	target.play();

	for (var i=0;i<100;i++) {
		await this.sleep(50);
		source.volume -= 0.01;
		target.volume += 0.01;
	}

	}

}
