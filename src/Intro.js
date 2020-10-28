import React, { Component } from 'react';
import styled from 'styled-components';
import { Link } from 'react-router-dom';

const Text = styled.body`
  font-family: Apercu, 'Source Sans Pro', system, system-ui, -apple-system,
    BlinkMacSystemFont, Roboto, Helvetica, Arial, sans-serif;
  line-height: 1.5;
`;
const Propic = styled.img`
  margin-left: 400px;
  height: 70%;
`;

export class Intro extends Component {
  render() {
    return (
      <Text>
        <section id='hero'>
          <div class='hero container'>
            <div>
              <h1>
                Hello, <span></span>
              </h1>
              <h1>
                I'm <span></span>
              </h1>
              <h1>
                Daniel <span></span>
              </h1>
              <a href='#' type='button' class='cta'>
                Blog
              </a>
              <a href='#' type='button' class='cta2'>
                Music
              </a>
            </div>
            <Propic src={require('./assets/img/thinking.jpg')} />
          </div>
        </section>
      </Text>
    );
  }
}

export default Intro;
