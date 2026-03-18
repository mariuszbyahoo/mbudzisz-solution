import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting, HttpTestingController } from '@angular/common/http/testing';
import { provideRouter, ActivatedRoute, convertToParamMap } from '@angular/router';
import { OrganizationDetailComponent } from './organization-detail.component';
import { OrganizationSummary } from '../../shared/models';

const MOCK_SUMMARY: OrganizationSummary = {
  organization: {
    id: 'org-001',
    name: 'Acme Corp',
    industry: 'Technology',
    tier: 'enterprise',
    contactEmail: 'partnerships@acme.com',
  },
  projectCount: 3,
  userCount: 5,
  totalInvoiced: 57000,
  currency: 'USD',
};

const activatedRouteMock = {
  snapshot: { paramMap: convertToParamMap({ id: 'org-001' }) },
};

describe('OrganizationDetailComponent', () => {
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OrganizationDetailComponent],
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
    const fixture = TestBed.createComponent(OrganizationDetailComponent);
    fixture.detectChanges();

    expect(fixture.nativeElement.querySelector('.loading')).toBeTruthy();

    httpMock.expectOne('/api/organizations/org-001/summary').flush(MOCK_SUMMARY);
  });

  it('should render the organization name as the page heading', () => {
    const fixture = TestBed.createComponent(OrganizationDetailComponent);
    fixture.detectChanges();
    httpMock.expectOne('/api/organizations/org-001/summary').flush(MOCK_SUMMARY);
    fixture.detectChanges();

    const h1 = fixture.nativeElement.querySelector('h1');
    expect(h1?.textContent?.trim()).toBe('Acme Corp');
  });

  it('should render organization details: industry, tier, and contact email', () => {
    const fixture = TestBed.createComponent(OrganizationDetailComponent);
    fixture.detectChanges();
    httpMock.expectOne('/api/organizations/org-001/summary').flush(MOCK_SUMMARY);
    fixture.detectChanges();

    const el = fixture.nativeElement as HTMLElement;
    expect(el.textContent).toContain('Technology');
    expect(el.textContent).toContain('enterprise');
    expect(el.textContent).toContain('partnerships@acme.com');
  });

  it('should render the stats cards with projectCount, userCount, and totalInvoiced', () => {
    const fixture = TestBed.createComponent(OrganizationDetailComponent);
    fixture.detectChanges();
    httpMock.expectOne('/api/organizations/org-001/summary').flush(MOCK_SUMMARY);
    fixture.detectChanges();

    const el = fixture.nativeElement as HTMLElement;
    const cards = Array.from(el.querySelectorAll('.card')) as HTMLElement[];
    const cardValue = (heading: string) =>
      cards.find(c => c.querySelector('h2')?.textContent?.trim() === heading)
        ?.querySelector('p')?.textContent?.trim() ?? '';

    expect(cardValue('Projects')).toBe('3');
    expect(cardValue('Users')).toBe('5');
    expect(cardValue('Total invoiced')).toContain('57');
    expect(cardValue('Total invoiced')).toContain('USD');
  });

  it('should render a back link to the organizations list', () => {
    const fixture = TestBed.createComponent(OrganizationDetailComponent);
    fixture.detectChanges();
    httpMock.expectOne('/api/organizations/org-001/summary').flush(MOCK_SUMMARY);
    fixture.detectChanges();

    const el = fixture.nativeElement as HTMLElement;
    expect(el.textContent).toContain('← Organizations');
  });

  it('should show an error card when the API fails', () => {
    const fixture = TestBed.createComponent(OrganizationDetailComponent);
    fixture.detectChanges();
    httpMock.expectOne('/api/organizations/org-001/summary').flush('Not Found', { status: 404, statusText: 'Not Found' });
    fixture.detectChanges();

    const el = fixture.nativeElement as HTMLElement;
    expect(el.querySelector('.error')).toBeTruthy();
    expect(el.querySelector('.loading')).toBeFalsy();
  });
});
