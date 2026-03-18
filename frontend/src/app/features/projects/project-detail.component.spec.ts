import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting, HttpTestingController } from '@angular/common/http/testing';
import { provideRouter, ActivatedRoute, convertToParamMap } from '@angular/router';
import { ProjectDetailComponent } from './project-detail.component';
import { ProjectDetail } from '../../shared/models';

const MOCK_PROJECT: ProjectDetail = {
  id: 'proj-001',
  name: 'Platform Modernization',
  status: 'active',
  budgetHours: 800,
  startDate: '2024-01-01',
  endDate: '2024-06-30',
  totalHoursLogged: 46,
  organization: { id: 'org-001', name: 'Acme Corp' },
};

const activatedRouteMock = {
  snapshot: { paramMap: convertToParamMap({ id: 'proj-001' }) },
};

describe('ProjectDetailComponent', () => {
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProjectDetailComponent],
      providers: [
        provideHttpClient(),
        provideHttpClientTesting(),
        provideRouter([]),
        { provide: ActivatedRoute, useValue: activatedRouteMock },
      ],
    }).compileComponents();

    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => httpMock.verify());

  it('should show loading state before the API responds', () => {
    const fixture = TestBed.createComponent(ProjectDetailComponent);
    fixture.detectChanges();

    expect(fixture.nativeElement.querySelector('.loading')).toBeTruthy();

    httpMock.expectOne('/api/projects/proj-001').flush(MOCK_PROJECT);
  });

  it('should render the project name as the page heading', () => {
    const fixture = TestBed.createComponent(ProjectDetailComponent);
    fixture.detectChanges();
    httpMock.expectOne('/api/projects/proj-001').flush(MOCK_PROJECT);
    fixture.detectChanges();

    const h1 = fixture.nativeElement.querySelector('h1');
    expect(h1?.textContent?.trim()).toBe('Platform Modernization');
  });

  it('should render status, budget, hours logged, organization, and dates', () => {
    const fixture = TestBed.createComponent(ProjectDetailComponent);
    fixture.detectChanges();
    httpMock.expectOne('/api/projects/proj-001').flush(MOCK_PROJECT);
    fixture.detectChanges();

    const el = fixture.nativeElement as HTMLElement;
    expect(el.textContent).toContain('active');
    expect(el.textContent).toContain('800 hours');
    expect(el.textContent).toContain('46');
    expect(el.textContent).toContain('Acme Corp');
    expect(el.textContent).toContain('2024-01-01');
    expect(el.textContent).toContain('2024-06-30');
  });

  it('should show — for null dates and 0 for null hours logged', () => {
    const noDateProject: ProjectDetail = {
      ...MOCK_PROJECT,
      startDate: null,
      endDate: null,
      totalHoursLogged: null,
      organization: null,
    };

    const fixture = TestBed.createComponent(ProjectDetailComponent);
    fixture.detectChanges();
    httpMock.expectOne('/api/projects/proj-001').flush(noDateProject);
    fixture.detectChanges();

    const el = fixture.nativeElement as HTMLElement;
    expect(el.textContent).toContain('— to —');
    expect(el.textContent).toContain('Hours logged:');
    // null ?? 0 renders as 0
    expect(el.textContent).toMatch(/Hours logged:\s*0/);
  });

  it('should render a back link to the projects list', () => {
    const fixture = TestBed.createComponent(ProjectDetailComponent);
    fixture.detectChanges();
    httpMock.expectOne('/api/projects/proj-001').flush(MOCK_PROJECT);
    fixture.detectChanges();

    expect(fixture.nativeElement.textContent).toContain('← Projects');
  });

  it('should show an error card when the API fails', () => {
    const fixture = TestBed.createComponent(ProjectDetailComponent);
    fixture.detectChanges();
    httpMock.expectOne('/api/projects/proj-001').flush('Not Found', { status: 404, statusText: 'Not Found' });
    fixture.detectChanges();

    const el = fixture.nativeElement as HTMLElement;
    expect(el.querySelector('.error')).toBeTruthy();
    expect(el.querySelector('.loading')).toBeFalsy();
  });
});
