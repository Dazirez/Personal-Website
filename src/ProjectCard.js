import React from 'react';
import { Card, Button } from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
import CardActionArea from '@material-ui/core/CardActionArea';
import CardMedia from '@material-ui/core/CardMedia';
import styled from 'styled-components';

const Link = styled.a`
  text-decoration: none;
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
      <Card>
        <CardActionArea className={classes.img}>
          <CardMedia style={{ height: '150px' }} image={imgSrc} title={title} />
        </CardActionArea>
      </Card>
      <br></br>
      <Button className={classes.button} variant='contained' color={primary}>
        {title}
      </Button>
      <Button variant='contained' color='secondary'>
        <Link href={link} target='none'>
          Github
        </Link>
      </Button>
    </div>
  );
};

export default ProjectCard;
