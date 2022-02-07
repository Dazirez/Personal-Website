import React from 'react';
import { Card, Button } from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
import CardActionArea from '@material-ui/core/CardActionArea';
import CardMedia from '@material-ui/core/CardMedia';
import styled from 'styled-components';

const Link = styled.a`
  text-decoration: none;
`;

const Title = styled.h1`
  padding: 10px; 
  padding-left: 20px; 
  font-size: 20px; 
`;
const useStyles = makeStyles({
  img: {
    '&:hover': {
      transform: 'scale(1.1)',
    },
  },
  button: {
    marginRight: '20px',
  },
});
const primary = '#3f51b5';

const ProjectCard = (props) => {
  const classes = useStyles();

  const { title, link, imgSrc } = props;
  return (
    <div>
      <Card style={{width: '15vw'}}>
        <CardActionArea className={classes.img}>
          <Title> {title}</Title>
          <a href={link} target='_blank'>
          <CardMedia style={{ height: '150px' }} image={imgSrc} title={title} />
          </a>
        </CardActionArea>
      </Card>
      <br></br>
   
    </div>
  );
};

export default ProjectCard;
