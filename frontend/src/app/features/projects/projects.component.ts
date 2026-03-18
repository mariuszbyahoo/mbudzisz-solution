import { ChangeDetectionStrategy, Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ApiService } from '../../core/api.service';
import { Project } from '../../shared/models';

@Component({
  selector: 'app-projects',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [RouterLink],
  template: `
    @if (error()) {
      <div class="card error">Error: {{ error() }}</div>
    } @else if (!list()) {
      <div class="card loading">Loading…</div>
    } @else {
      <div>
        <h1>Projects</h1>
        <div class="grid grid-2">
          @for (proj of list(); track proj.id) {
            <a [routerLink]="['/projects', proj.id]" class="card-link">
              <div class="card">
                <h2>{{ proj.name }}</h2>
                <p><span class="badge badge--{{ proj.status }}">{{ proj.status }}</span></p>
                <p>Budget: {{ proj.budgetHours }}h · {{ proj.startDate ?? '—' }} to {{ proj.endDate ?? '—' }}</p>
              </div>
            </a>
          }
        </div>
      </div>
    }
  `
})
export class ProjectsComponent implements OnInit {
  private api = inject(ApiService);
  private destroyRef = inject(DestroyRef);

  list = signal<Project[] | null>(null);
  error = signal<string | null>(null);

  ngOnInit(): void {
    this.api.getProjects()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (list) => this.list.set(list),
        error: (err) => this.error.set(err.message ?? 'Unknown error')
      });
  }
}
