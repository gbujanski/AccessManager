import { ComponentFixture, TestBed } from '@angular/core/testing';
import { UsersService } from './usersService';

describe('UsersService', () => {
  let component: UsersService;
  let fixture: ComponentFixture<UsersService>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UsersService],
    }).compileComponents();

    fixture = TestBed.createComponent(UsersService);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
