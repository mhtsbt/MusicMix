"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
const platform_browser_1 = require("@angular/platform-browser");
const core_1 = require("@angular/core");
const http_1 = require("@angular/http");
const AppComponent_1 = require("./Components/AppComponent");
const ControlsComponent_1 = require("./Components/ControlsComponent");
const PlayerComponent_1 = require("./Components/PlayerComponent");
const SongService_1 = require("./Services/SongService");
let AppModule = class AppModule {
};
AppModule = __decorate([
    core_1.NgModule({
        declarations: [AppComponent_1.AppComponent, ControlsComponent_1.ControlsComponent, PlayerComponent_1.PlayerComponent],
        imports: [platform_browser_1.BrowserModule, http_1.HttpModule],
        providers: [SongService_1.SongService],
        bootstrap: [AppComponent_1.AppComponent]
    })
], AppModule);
exports.AppModule = AppModule;
//# sourceMappingURL=AppModule.js.map