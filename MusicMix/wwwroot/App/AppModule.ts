import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from "@angular/http";

import { AppComponent } from "./Components/AppComponent";
import { ControlsComponent } from "./Components/ControlsComponent";
import { PlayerComponent } from "./Components/PlayerComponent";
import { SongService } from "./Services/SongService";

@NgModule({
    declarations: [AppComponent, ControlsComponent, PlayerComponent],
    imports: [BrowserModule, HttpModule],
    providers: [SongService],
    bootstrap: [AppComponent]
})
export class AppModule { }
