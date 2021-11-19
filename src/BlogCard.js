import React, { Component } from 'react';
import styled from 'styled-components';
import {Link} from 'react-router-dom';

const Container = styled.div`
  width: 30vw;
  height: 30vh;
  box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.6);
  transition: 0.4s;
  background: #fff;
  font-size: 16px;
  font-family: sans-serif;
  margin: 30px;
  &:hover {
    width: 31vw;
    height: 40vh;
  }
`;
const PreviewImage = styled.img``;
const Title = styled.h2`
  color: #000;
  text-decoration: none;
`;

export class BlogCard extends Component {
  render() {
    return (
      <Container>
        <Link to='/'>
          <PreviewImage src={this.props.imgURL} />
          <Title>{this.props.title}</Title>
        </Link>
      </Container>
    );
  }
}

export default BlogCard;
