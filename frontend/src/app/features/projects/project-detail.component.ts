import { ChangeDetectionStrategy, Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { RouterLink, ActivatedRoute } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ApiService } from '../../core/api.service';
import { ProjectDetail } from '../../shared/models';

@Component({
  selector: 'app-project-detail',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [RouterLink],
  template: `
    @let p = project();
    @if (error()) {
      <div class="card error">Error: {{ error() }}</div>
    } @else if (!p) {
      <div class="card loading">Loading…</div>
    } @else {
      <div>
        <p><a routerLink="/projects">← Projects</a></p>
        <h1>{{ p.name }}</h1>
        <div class="card">
          <p><strong>Status:</strong> {{ p.status }}</p>
          <p><strong>Budget:</strong> {{ p.budgetHours }} hours</p>
          <p><strong>Hours logged:</strong> {{ p.totalHoursLogged ?? 0 }}</p>
          <p><strong>Organization:</strong> {{ p.organization?.name }}</p>
          <p><strong>Dates:</strong> {{ p.startDate ?? '—' }} to {{ p.endDate ?? '—' }}</p>
        </div>
      </div>
    }
  `
})
export class ProjectDetailComponent implements OnInit {
  private api = inject(ApiService);
  private route = inject(ActivatedRoute);
  private destroyRef = inject(DestroyRef);

  project = signal<ProjectDetail | null>(null);
  error = signal<string | null>(null);

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id')!;
    this.api.getProject(id)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (project) => this.project.set(project),
        error: (err) => this.error.set(err.message ?? 'Unknown error')
      });
  }
}
