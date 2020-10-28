import React, { Component } from 'react';
import { TwitchEmbed } from 'react-twitch-embed';
import styled from 'styled-components';

const Container = styled.div`
  width: 60%;
  margin: auto;
  margin-top: 50px;
`;
export class Stream extends Component {
  render() {
    return (
      <React.Fragment>
        <div id='Stream'>
          <h1 id='StreamHeader'>Hang Out With Me</h1>
          <Container>
            <TwitchEmbed channel='loldreamer' withChat={true} />
          </Container>
        </div>
      </React.Fragment>
    );
  }
}

export default Stream;
