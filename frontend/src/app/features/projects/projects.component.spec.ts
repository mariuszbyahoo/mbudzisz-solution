import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting, HttpTestingController } from '@angular/common/http/testing';
import { provideRouter } from '@angular/router';
import { ProjectsComponent } from './projects.component';
import { Project } from '../../shared/models';

const MOCK_PROJECTS: Project[] = [
  { id: 'proj-001', name: 'Platform Modernization', status: 'active', budgetHours: 800, startDate: '2024-01-01', endDate: '2024-06-30' },
  { id: 'proj-002', name: 'API Gateway', status: 'active', budgetHours: 200, startDate: '2024-02-01', endDate: null },
  { id: 'proj-003', name: 'Legacy Audit', status: 'completed', budgetHours: 120, startDate: '2023-11-01', endDate: '2023-12-15' },
];

describe('ProjectsComponent', () => {
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProjectsComponent],
      providers: [provideHttpClient(), provideHttpClientTesting(), provideRouter([])],
    }).compileComponents();

    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => httpMock.verify());

  it('should show loading state before the API responds', () => {
    const fixture = TestBed.createComponent(ProjectsComponent);
    fixture.detectChanges();

    expect(fixture.nativeElement.querySelector('.loading')).toBeTruthy();

    httpMock.expectOne('/api/projects').flush(MOCK_PROJECTS);
  });

  it('should render a card for each project', () => {
    const fixture = TestBed.createComponent(ProjectsComponent);
    fixture.detectChanges();
    httpMock.expectOne('/api/projects').flush(MOCK_PROJECTS);
    fixture.detectChanges();

    const cards = fixture.nativeElement.querySelectorAll('.card');
    expect(cards.length).toBe(MOCK_PROJECTS.length);
  });

  it('should render each project name', () => {
    const fixture = TestBed.createComponent(ProjectsComponent);
    fixture.detectChanges();
    httpMock.expectOne('/api/projects').flush(MOCK_PROJECTS);
    fixture.detectChanges();

    const el = fixture.nativeElement as HTMLElement;
    expect(el.textContent).toContain('Platform Modernization');
    expect(el.textContent).toContain('API Gateway');
    expect(el.textContent).toContain('Legacy Audit');
  });

  it('should render status badge and budget line for each project', () => {
    const fixture = TestBed.createComponent(ProjectsComponent);
    fixture.detectChanges();
    httpMock.expectOne('/api/projects').flush(MOCK_PROJECTS);
    fixture.detectChanges();

    const el = fixture.nativeElement as HTMLElement;
    const cards = Array.from(el.querySelectorAll('.card')) as HTMLElement[];

    // active project: badge class and budget
    const activeCard = cards.find(c => c.querySelector('h2')?.textContent?.trim() === 'Platform Modernization')!;
    expect(activeCard.querySelector('.badge--active')).toBeTruthy();
    expect(activeCard.textContent).toContain('800h');
    expect(activeCard.textContent).toContain('2024-01-01');

    // completed project: badge class
    const completedCard = cards.find(c => c.querySelector('h2')?.textContent?.trim() === 'Legacy Audit')!;
    expect(completedCard.querySelector('.badge--completed')).toBeTruthy();
  });

  it('should render — for a null end date', () => {
    const fixture = TestBed.createComponent(ProjectsComponent);
    fixture.detectChanges();
    httpMock.expectOne('/api/projects').flush(MOCK_PROJECTS);
    fixture.detectChanges();

    const el = fixture.nativeElement as HTMLElement;
    const cards = Array.from(el.querySelectorAll('.card')) as HTMLElement[];
    const apiGateway = cards.find(c => c.querySelector('h2')?.textContent?.trim() === 'API Gateway')!;
    expect(apiGateway.textContent).toContain('—');
  });

  it('should show an error card when the API fails', () => {
    const fixture = TestBed.createComponent(ProjectsComponent);
    fixture.detectChanges();
    httpMock.expectOne('/api/projects').flush('Server Error', { status: 500, statusText: 'Internal Server Error' });
    fixture.detectChanges();

    const el = fixture.nativeElement as HTMLElement;
    expect(el.querySelector('.error')).toBeTruthy();
    expect(el.querySelector('.loading')).toBeFalsy();
  });
});
