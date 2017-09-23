import { TestBed, inject } from '@angular/core/testing';

import { PlayermanagerService } from './playermanager.service';

describe('PlayermanagerService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PlayermanagerService]
    });
  });

  it('should be created', inject([PlayermanagerService], (service: PlayermanagerService) => {
    expect(service).toBeTruthy();
  }));
});
