import React, { Component } from 'react';
import styled from 'styled-components';
import { Link } from 'react-router-dom';

const Text = styled.body`
  font-family: Apercu, 'Source Sans Pro', system, system-ui, -apple-system,
    BlinkMacSystemFont, Roboto, Helvetica, Arial, sans-serif;
  line-height: 1.5;
`;
const Pic = styled.img`
  max-width: 50%;
  height: auto;
  bottom: 0;
  margin-top: auto;
  margin-left: auto;
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
              <a
                href='#'
                style={{ marginLeft: '10px' }}
                type='button'
                class='cta2'>
                Music
              </a>
            </div>
            <Pic src={require('./assets/img/thinking.jpg')} />
          </div>
        </section>
      </Text>
    );
  }
}

export default Intro;
