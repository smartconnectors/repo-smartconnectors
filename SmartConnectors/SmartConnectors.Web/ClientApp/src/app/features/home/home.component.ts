import { Component, OnInit } from '@angular/core';
import { HttpService } from '~/core/http';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';


@Component({ selector: 'app-home-component', templateUrl: './home.component.html', styleUrls: ['./home.component.scss'] })

export class HomeComponent implements OnInit {
    shouldRun = true;
    projectList = [];
    projectName: string = '';

    public constructor(private router: Router, private http: HttpService) { }

    public ngOnInit(): void {
        this.getProjects();
    }

    getProjects() {
        this.http.get(environment.baseUrl + '/projects', true).subscribe((res) => {
            this.projectList = res;
        });
    }

    navigate(projectId: number) {
        this.router.navigate(['/', 'project', projectId]);
    }

    createProject() {
        let body = {
            name: this.projectName
        }
        this.http.post(environment.baseUrl + '/projects', body, true).subscribe((res) => {
            this.getProjects();
        })
    }

    deleteProject(id) {
        this.http.del(environment.baseUrl + '/projects/' + id, true).subscribe((res) => {
            this.getProjects();
        });
    }
}

