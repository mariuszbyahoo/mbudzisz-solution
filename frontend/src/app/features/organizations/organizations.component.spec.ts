import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting, HttpTestingController } from '@angular/common/http/testing';
import { provideRouter } from '@angular/router';
import { OrganizationsComponent } from './organizations.component';
import { Organization } from '../../shared/models';

const MOCK_ORGS: Organization[] = [
  { id: 'org-001', name: 'Acme Corp', industry: 'Technology', tier: 'enterprise', contactEmail: 'partnerships@acme.com' },
  { id: 'org-002', name: 'Beta Industries', industry: 'Manufacturing', tier: 'professional', contactEmail: 'it@betaind.com' },
  { id: 'org-003', name: 'Gamma Labs', industry: 'Healthcare', tier: 'starter', contactEmail: 'ops@gammalabs.io' },
];

describe('OrganizationsComponent', () => {
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OrganizationsComponent],
      providers: [provideHttpClient(), provideHttpClientTesting(), provideRouter([])],
    }).compileComponents();

    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => httpMock.verify());

  it('should show loading state before the API responds', () => {
    const fixture = TestBed.createComponent(OrganizationsComponent);
    fixture.detectChanges();

    expect(fixture.nativeElement.querySelector('.loading')).toBeTruthy();

    httpMock.expectOne('/api/organizations').flush(MOCK_ORGS);
  });

  it('should render a card for each organization', () => {
    const fixture = TestBed.createComponent(OrganizationsComponent);
    fixture.detectChanges();
    httpMock.expectOne('/api/organizations').flush(MOCK_ORGS);
    fixture.detectChanges();

    const cards = fixture.nativeElement.querySelectorAll('.card');
    expect(cards.length).toBe(MOCK_ORGS.length);
  });

  it('should render each organization name, industry, tier, and email', () => {
    const fixture = TestBed.createComponent(OrganizationsComponent);
    fixture.detectChanges();
    httpMock.expectOne('/api/organizations').flush(MOCK_ORGS);
    fixture.detectChanges();

    const el = fixture.nativeElement as HTMLElement;

    expect(el.textContent).toContain('Acme Corp');
    expect(el.textContent).toContain('Beta Industries');
    expect(el.textContent).toContain('Gamma Labs');

    // first card: industry, tier badge, and contact email
    const firstCard = el.querySelectorAll('.card')[0];
    expect(firstCard.textContent).toContain('Technology');
    expect(firstCard.textContent).toContain('enterprise');
    expect(firstCard.textContent).toContain('partnerships@acme.com');
  });

  it('should apply the correct tier badge class', () => {
    const fixture = TestBed.createComponent(OrganizationsComponent);
    fixture.detectChanges();
    httpMock.expectOne('/api/organizations').flush(MOCK_ORGS);
    fixture.detectChanges();

    const el = fixture.nativeElement as HTMLElement;
    expect(el.querySelector('.badge--enterprise')).toBeTruthy();
    expect(el.querySelector('.badge--professional')).toBeTruthy();
    expect(el.querySelector('.badge--starter')).toBeTruthy();
  });

  it('should show an error card when the API fails', () => {
    const fixture = TestBed.createComponent(OrganizationsComponent);
    fixture.detectChanges();
    httpMock.expectOne('/api/organizations').flush('Server Error', { status: 500, statusText: 'Internal Server Error' });
    fixture.detectChanges();

    const el = fixture.nativeElement as HTMLElement;
    expect(el.querySelector('.error')).toBeTruthy();
    expect(el.querySelector('.loading')).toBeFalsy();
  });
});
