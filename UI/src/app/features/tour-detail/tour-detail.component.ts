import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { TourService } from '../Tour/services/tour.service';
import { CommonModule } from '@angular/common';
import { TourVm } from '../Tour/models/tourVm.model';



@Component({
  selector: 'app-tour-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './tour-detail.component.html',
  styleUrls: ['./tour-detail.component.css']
})
export class TourDetailComponent implements OnInit, OnDestroy {
  tourVm?: TourVm;
  registerSubscription?: Subscription;
  tourId!: number;

  constructor(private route: ActivatedRoute, private tourService: TourService) { }

  ngOnDestroy(): void {
    this.registerSubscription?.unsubscribe();
  }

  fetchTourDetails(id: number): void {
    this.registerSubscription = this.tourService.getTourVmById(id).subscribe({
      next: (tourVm) => {
        this.tourVm = tourVm;
        console.log('Fetched Tour Details:', this.tourVm);
      },
      error: (err) => console.error('Error fetching tour details:', err)
    });
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      console.log('Tour ID from params:', id);
      if (id) {
        this.tourId = +id;
        this.fetchTourDetails(this.tourId);
      }
    });
  }
}
