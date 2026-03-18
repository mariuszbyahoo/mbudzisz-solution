import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting, HttpTestingController } from '@angular/common/http/testing';
import { DashboardComponent } from './dashboard.component';
import { DashboardData } from '../../shared/models';

const SEED_DASHBOARD: DashboardData = {
  totalOrganizations: 4,
  totalUsers: 8,
  totalProjects: 6,
  activeProjects: 4,
  totalTimeEntries: 12,
  totalInvoiced: 137000
};

describe('DashboardComponent', () => {
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DashboardComponent],
      providers: [provideHttpClient(), provideHttpClientTesting()]
    }).compileComponents();

    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => httpMock.verify());

  it('should fetch dashboard data from the API on init', () => {
    const fixture = TestBed.createComponent(DashboardComponent);
    fixture.detectChanges();

    // Exactly one request should be issued to the correct endpoint
    const req = httpMock.expectOne('/api/dashboard');
    expect(req.request.method).toBe('GET');
    req.flush(SEED_DASHBOARD);
  });

  it('should show loading state before the API responds', () => {
    const fixture = TestBed.createComponent(DashboardComponent);
    fixture.detectChanges();

    const el = fixture.nativeElement as HTMLElement;
    expect(el.querySelector('.loading')).toBeTruthy();

    httpMock.expectOne('/api/dashboard').flush(SEED_DASHBOARD);
  });

  it('should render all metric cards with values from the API response', () => {
    const fixture = TestBed.createComponent(DashboardComponent);
    fixture.detectChanges();
    httpMock.expectOne('/api/dashboard').flush(SEED_DASHBOARD);
    fixture.detectChanges();

    const el = fixture.nativeElement as HTMLElement;
    const cards = Array.from(el.querySelectorAll('.card')) as HTMLElement[];

    const cardValue = (heading: string): string =>
      cards
        .find(c => c.querySelector('h2')?.textContent?.trim() === heading)
        ?.querySelector('p')?.textContent?.trim() ?? '';

    expect(cardValue('Organizations')).toBe('4');
    expect(cardValue('Users')).toBe('8');
    expect(cardValue('Projects')).toContain('6');
    expect(cardValue('Projects')).toContain('4 active');
    expect(cardValue('Time entries')).toBe('12');
    // totalInvoiced: check for the number value without locale-format assumptions
    expect(cardValue('Total invoiced')).toContain('137');
  });

  it('should show an error card when the API returns an error', () => {
    const fixture = TestBed.createComponent(DashboardComponent);
    fixture.detectChanges();
    httpMock.expectOne('/api/dashboard').flush('Internal Server Error', {
      status: 500,
      statusText: 'Internal Server Error'
    });
    fixture.detectChanges();

    const el = fixture.nativeElement as HTMLElement;
    expect(el.querySelector('.error')).toBeTruthy();
    expect(el.querySelector('.loading')).toBeFalsy();
  });
});
