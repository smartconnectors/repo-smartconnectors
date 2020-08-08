import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-shell-component',
  templateUrl: './shell.component.html',
  styleUrls: ['./shell.component.scss']
})

export class ShellComponent {
  isAuthenticated: boolean = false;
  isExpanded: boolean = false;

  menus = [
    {
      path: '/home',
      title: 'Home',
      icon: 'home',
      isActive: true
    }
  ];

  loggedInUserName = '--';
  userShortName = '--';

  constructor(
    private router: Router
  ) { }

  ngOnInit() {

  }

  // navigation click actions
  navClick(route) {
    if (route) {
      this.menus.forEach(item => {
        item.title === route.title ? item.isActive = true : item.isActive = false;
      });
    }
  }

  toggleMenu() {
    this.isExpanded = !this.isExpanded;

    if (this.isExpanded) {
      document.body.classList.add('app-left-navigation-expanded');
    } else {
      document.body.classList.remove('app-left-navigation-expanded');
    }
  }
}


