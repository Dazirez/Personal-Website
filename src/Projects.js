import React, { Component } from 'react';
import ProjectCard from './ProjectCard';
import styled from 'styled-components'; 

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
          link='https://docs.google.com/document/d/1d4EzTnX8maxcW0L8ZaT1ndyi4Pi-r-nonAVtnAbm39Q/edit?usp=sharing'
          subheader='Teaching and Streaming Service for Students'
        />
        <ProjectCard
          title='Penguin Passing'
          imgSrc={require('./assets/img/penguin.gif')}
          link='https://penguinpassing.itch.io/penguin-passing'
        />
        <ProjectCard
          title='CloudFront'
          imgSrc={require('./assets/img/CloudFront.png')}
          link='http://www-personal.umich.edu/~dbzheng/WebBuild/'
        />
        <ProjectCard
          title='Zelda NES Clone'
          imgSrc={require('./assets/img/link.gif')}
          link='http://www-personal.umich.edu/~dbzheng/Zelda/'
        />
      </div>
    );
  }
}

export default Projects;
