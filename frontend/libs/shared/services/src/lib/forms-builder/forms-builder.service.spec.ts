import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsBuilderService } from './forms-builder.service';

describe('FormsBuilderService', () => {
  let component: FormsBuilderService;
  let fixture: ComponentFixture<FormsBuilderService>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormsBuilderService],
    }).compileComponents();

    fixture = TestBed.createComponent(FormsBuilderService);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
