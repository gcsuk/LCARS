import React, { useState } from 'react';

export default function Today() {

  const date = new Date();
  const [today, setToday] = useState(date.getDate() + '-' + (date.getMonth() + 1) + '-' +  date.getFullYear());

  return (
    <span>{today}</span>
  );
}