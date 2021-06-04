import React, { Component } from 'react';
import styled from 'styled-components';
import { Link } from 'react-router-dom';
import { SocialIcon } from 'react-social-icons';

const Text = styled.body`
  font-family: Apercu, 'Source Sans Pro', system, system-ui, -apple-system,
    BlinkMacSystemFont, Roboto, Helvetica, Arial, sans-serif;
  line-height: 1.5;
`;
const Pic = styled.img`
  max-width: 50%;
  height: auto;
  top: 0;
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
              <SocialIcon network="github" url="https://github.com/Dazirez" style={{ marginTop: 25}}/>
              <SocialIcon network="linkedin" url="https://www.linkedin.com/in/dbzheng/" style={{ marginLeft: 25, marginTop: 25}}/>
              <SocialIcon network="youtube" url="https://www.youtube.com/channel/UCcCYKEueIGUsdsnWm1_ZY8g" style={{ marginLeft: 25, marginTop: 25}}/>
              <SocialIcon network="twitch" url="https://www.twitch.tv/loldreamer/" style={{ marginLeft: 25, marginTop: 25}}/>

            </div>
            <Pic src={require('./assets/img/propic-2.jpeg')} />
          </div>
        </section>
      </Text>
    );
  }
}

export default Intro;
