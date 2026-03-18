import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting, HttpTestingController } from '@angular/common/http/testing';
import { ApiService } from './api.service';
import {
  DashboardData,
  Organization,
  OrganizationSummary,
  Project,
  ProjectDetail,
} from '../shared/models';

describe('ApiService', () => {
  let service: ApiService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [provideHttpClient(), provideHttpClientTesting()],
    });
    service = TestBed.inject(ApiService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => httpMock.verify());

  // ---------------------------------------------------------------------------
  // Dashboard
  // ---------------------------------------------------------------------------

  describe('getDashboard', () => {
    const mockData: DashboardData = {
      totalOrganizations: 4,
      totalUsers: 8,
      totalProjects: 6,
      activeProjects: 4,
      totalTimeEntries: 12,
      totalInvoiced: 137000,
    };

    it('should issue GET /api/dashboard', () => {
      service.getDashboard().subscribe();

      const req = httpMock.expectOne('/api/dashboard');
      expect(req.request.method).toBe('GET');
      req.flush(mockData);
    });

    it('should return the response body as DashboardData', () => {
      let result: DashboardData | undefined;
      service.getDashboard().subscribe((d) => (result = d));

      httpMock.expectOne('/api/dashboard').flush(mockData);

      expect(result).toEqual(mockData);
    });
  });

  // ---------------------------------------------------------------------------
  // Organizations
  // ---------------------------------------------------------------------------

  describe('getOrganizations', () => {
    const mockList: Organization[] = [
      { id: 'org-001', name: 'Acme Corp', industry: 'Technology', tier: 'enterprise', contactEmail: 'a@acme.com' },
      { id: 'org-002', name: 'Beta Inc', industry: 'Manufacturing', tier: 'professional', contactEmail: 'b@beta.com' },
    ];

    it('should issue GET /api/organizations', () => {
      service.getOrganizations().subscribe();

      const req = httpMock.expectOne('/api/organizations');
      expect(req.request.method).toBe('GET');
      req.flush(mockList);
    });

    it('should return the response body as Organization[]', () => {
      let result: Organization[] | undefined;
      service.getOrganizations().subscribe((list) => (result = list));

      httpMock.expectOne('/api/organizations').flush(mockList);

      expect(result).toEqual(mockList);
      expect(result?.length).toBe(2);
    });
  });

  describe('getOrganizationSummary', () => {
    const mockSummary: OrganizationSummary = {
      organization: { id: 'org-001', name: 'Acme Corp', industry: 'Technology', tier: 'enterprise', contactEmail: 'a@acme.com' },
      projectCount: 3,
      userCount: 5,
      totalInvoiced: 57000,
      currency: 'USD',
    };

    it('should issue GET /api/organizations/:id/summary with the correct id', () => {
      service.getOrganizationSummary('org-001').subscribe();

      const req = httpMock.expectOne('/api/organizations/org-001/summary');
      expect(req.request.method).toBe('GET');
      req.flush(mockSummary);
    });

    it('should interpolate different ids into the path', () => {
      service.getOrganizationSummary('org-042').subscribe();

      const req = httpMock.expectOne('/api/organizations/org-042/summary');
      expect(req.request.method).toBe('GET');
      req.flush(mockSummary);
    });

    it('should return the response body as OrganizationSummary', () => {
      let result: OrganizationSummary | undefined;
      service.getOrganizationSummary('org-001').subscribe((s) => (result = s));

      httpMock.expectOne('/api/organizations/org-001/summary').flush(mockSummary);

      expect(result).toEqual(mockSummary);
    });
  });

  // ---------------------------------------------------------------------------
  // Projects
  // ---------------------------------------------------------------------------

  describe('getProjects', () => {
    const mockList: Project[] = [
      { id: 'proj-001', name: 'Platform Modernization', status: 'active', budgetHours: 800, startDate: '2024-01-01', endDate: '2024-06-30' },
      { id: 'proj-002', name: 'API Gateway', status: 'active', budgetHours: 200, startDate: '2024-02-01', endDate: null },
    ];

    it('should issue GET /api/projects', () => {
      service.getProjects().subscribe();

      const req = httpMock.expectOne('/api/projects');
      expect(req.request.method).toBe('GET');
      req.flush(mockList);
    });

    it('should return the response body as Project[]', () => {
      let result: Project[] | undefined;
      service.getProjects().subscribe((list) => (result = list));

      httpMock.expectOne('/api/projects').flush(mockList);

      expect(result).toEqual(mockList);
      expect(result?.length).toBe(2);
    });
  });

  describe('getProject', () => {
    const mockDetail: ProjectDetail = {
      id: 'proj-001',
      name: 'Platform Modernization',
      status: 'active',
      budgetHours: 800,
      startDate: '2024-01-01',
      endDate: '2024-06-30',
      totalHoursLogged: 46,
      organization: { id: 'org-001', name: 'Acme Corp' },
    };

    it('should issue GET /api/projects/:id with the correct id', () => {
      service.getProject('proj-001').subscribe();

      const req = httpMock.expectOne('/api/projects/proj-001');
      expect(req.request.method).toBe('GET');
      req.flush(mockDetail);
    });

    it('should interpolate different ids into the path', () => {
      service.getProject('proj-099').subscribe();

      const req = httpMock.expectOne('/api/projects/proj-099');
      expect(req.request.method).toBe('GET');
      req.flush(mockDetail);
    });

    it('should return the response body as ProjectDetail', () => {
      let result: ProjectDetail | undefined;
      service.getProject('proj-001').subscribe((p) => (result = p));

      httpMock.expectOne('/api/projects/proj-001').flush(mockDetail);

      expect(result).toEqual(mockDetail);
    });

    it('should handle a project with null organization and null dates', () => {
      const minimal: ProjectDetail = {
        id: 'proj-005',
        name: 'Compliance Dashboard',
        status: 'draft',
        budgetHours: 180,
        startDate: null,
        endDate: null,
        totalHoursLogged: null,
        organization: null,
      };

      let result: ProjectDetail | undefined;
      service.getProject('proj-005').subscribe((p) => (result = p));

      httpMock.expectOne('/api/projects/proj-005').flush(minimal);

      expect(result?.organization).toBeNull();
      expect(result?.startDate).toBeNull();
      expect(result?.totalHoursLogged).toBeNull();
    });
  });
});
