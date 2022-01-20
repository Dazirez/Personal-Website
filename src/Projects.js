import React, { Component } from 'react';
import ProjectCard from './ProjectCard';

export class Projects extends Component {
  render() {
    return (
      <div id='ProjectsSection'>
        <ProjectCard
          title='Outlaws'
          imgSrc={require('./assets/img/Outlaws.gif')}
          link='https://yeehaw-frontend.herokuapp.com/'
        />
        <ProjectCard
          title='Offhours'
          imgSrc={require('./assets/img/Offhours.png')}
          link='https://github.com/Dazirez/offhours4'
          subheader='Teaching and Streaming Service for Students'
        />

        <ProjectCard
          title='Mentii'
          imgSrc={require('./assets/img/Mentii.png')}
          link='https://github.com/Dazirez/mentii-2'
        />
        <ProjectCard
          title='IG Clone'
          imgSrc={require('./assets/img/instagram.png')}
          link='https://github.com/Dazirez/mentii-2'
        />
        <ProjectCard
          title='Parade'
          imgSrc={require('./assets/img/parade.png')}
          link='https://github.com/Dazirez/mentii-2'
        />
      </div>
    );
  }
}

export default Projects;
