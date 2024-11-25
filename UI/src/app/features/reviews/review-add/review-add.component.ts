import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { Subject, Subscription } from 'rxjs';
import { ReviewService } from '../services/review.service';
import { CretaReview } from '../model/create-review.model';
import { AuthService } from '../../auth/services/auth.service';
import { StatusType } from '../../Booking/models/booking.model';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-review-add',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './review-add.component.html',
  styleUrls: ['./review-add.component.css']
})
export class ReviewAddComponent implements OnInit, OnDestroy {

  private destroy$ = new Subject<void>();
  rating?: number;
  comment?: string;
  tourId?: number;
  bookingId?: number;
  registerSubscription?: Subscription;

  constructor(
    private reviewService: ReviewService,
    private auth: AuthService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {

    this.route.params.subscribe(params => {
      this.tourId = +params['tourId'];
      this.bookingId = +params['bookingId'];

      console.log('Tour ID:', this.tourId);
      console.log('Booking ID:', this.bookingId);
    });
  }


  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
    if (this.registerSubscription) {
      this.registerSubscription.unsubscribe();
    }
  }

  onSubmit(): void {
    console.log('Form values before submission:');
    console.log('Rating:', this.rating);
    console.log('Comment:', this.comment);
    console.log('Tour ID:', this.tourId);

    // Validate input fields
    if (this.rating === undefined || this.comment === undefined || this.rating < 1 || this.rating > 5) {
      Swal.fire({
        icon: 'error',
        title: 'Invalid Input!',
        text: 'Please fill in all fields correctly and provide a rating between 1 and 5.',
      });
      return;
    }

    // Check if tourId is valid
    if (!this.tourId) {
      Swal.fire({
        icon: 'error',
        title: 'Invalid Tour ID!',
        text: 'There was an issue retrieving the Tour ID. Please try again later.',
      });
      return;
    }

    const sessionUserId = this.auth.getUserId();

    const reviewData: CretaReview = {
      tourId: this.tourId,
      userId: sessionUserId,
      bookingId: this.bookingId,
      rating: this.rating,
      comment: this.comment,
      reviewDate: new Date().toISOString(),
      status: StatusType.Available,
    };

    console.log('Review Submitted:', reviewData);

    this.registerSubscription = this.reviewService.insert(reviewData).subscribe({
      next: (response) => {
        console.log('Review submitted successfully', response);
        Swal.fire({
          icon: 'success',
          title: 'Cảm ơn bạn đã gửi đánh giá!',
          text: 'Đánh giá của bạn đã được gửi thành công.',
        }).then(() => {
          // Navigate to the profile page after successful review submission
          this.router.navigate(['/profile']);
        });
      },
      error: (error) => {
        console.error('Error submitting review:', error);
        Swal.fire({
          icon: 'error',
          title: 'Error!',
          text: 'There was an error submitting your review. Please try again later.',
        });
      }
    });
  }
}
